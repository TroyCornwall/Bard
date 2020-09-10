# Story

As described in the [introduction](../../) the story is where the work is actually done where we can perform our test arrangement. 

We can have a few different flavors of stories depending upon where we are in the scenario and how we want to use the story.

## Starting Story

A starting story is a story that is written in the opening chapter of a `StoryBook`. It is different from other stories in the fact that it does not have any input from other stories.

## Simple Story

A simple story has come from another chapter and therefore has input from the previous story.

Notice we have access to the context and the bankAccount on line 3.

```csharp
public DepositMade Make_deposit(decimal amount)
{
    return When((context) =>
        {
            var request = new Deposit{ Amount = amount};
            var response = context.Api.Post($"api/bankaccounts/{bankAccount.Id}/deposits", request);
            context.StoryData.Deposit = response;           
        })
        .ProceedToChapter<DepositMade>();
}
```

## Advanced Story

Sometimes in the course of a scenario that it makes sense that we want to reuse our story in another chapter. For example with our Banking example after we create a bank account we can either make a withdrawal or a deposit. If we make a withdrawal we should be able to make a further withdrawal or a further deposit. In essence we want to be able to recursively make withdrawals and deposits using our fluent interface. 

We could just copy and paste the code into each chapter but that could present a maintenance problem in the future. 

However because Bard takes a functional approach to describing it's stories we can define a common library of Story functions and compose our Chapter with those functions.

Lets demonstrate with an example:

```csharp
public static class BankingScenarioFunctions
{
    public static readonly Action<ScenarioContext<BankingStoryData>, Deposit> MakeADeposit =
            (context, request) =>
            {
                context.Api.Post($"api/bankaccounts/{context.StoryData?.BankAccountId}/deposits",
                    request);
            };
}
```

What does this mean? 

* **ScenarioContext** this is our test context.
* **DepositRequest** this is the API parameter that is passed into the function which means we can pass it from our story rather than hard code it in this function.

Now in our Chapter we write our Story by referencing our Function

```csharp
public DepositMade Deposit_has_been_made(decimal amount)
{
    return
        Given((storyData) => new Deposit {Amount = amount})
            .When(BankingScenarioFunctions.MakeADeposit)
            .ProceedToChapter<DepositMade>();
}
```

Notice that on line 4 we are calling the base Method `Given` and passing in a function that describes how to create our API request object.

