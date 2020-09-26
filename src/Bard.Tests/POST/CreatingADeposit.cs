﻿using Bard.Sample.Api.Model;
using Bard.Tests.Scenario;
using Xunit;
using Xunit.Abstractions;

namespace Bard.Tests.POST
{
    public class MakingADeposit : BankingTestBase
    {
        public MakingADeposit(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void If_a_deposit_is_made_for_bank_account_that_does_not_exist_then_a_404_should_be_returned()
        {
            Given
                .Nothing_much_happens();

            When
                .Post("api/bankaccounts/1234/deposits", new Deposit {Amount = 100});

            Then
                .Response
                .ShouldBe
                .NotFound();
        }

        [Fact]
        public void If_the_deposit_is_successful_then_an_ok_response_should_be_returned()
        {
            Given
                .BankAccount_has_been_created()
                .GetResult(out BankingStoryData bankAccount);

            When
                .Post($"api/bankaccounts/{bankAccount.BankAccountId}/deposits", new Deposit {Amount = 100});

            Then.Response.ShouldBe.Ok();
        }
    }
}