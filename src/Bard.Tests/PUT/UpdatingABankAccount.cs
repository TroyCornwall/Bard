﻿using Bard.Sample.Api.Model;
using Bard.Tests.Scenario;
using Xunit;
using Xunit.Abstractions;

namespace Bard.Tests.PUT
{
    public class UpdatingABankAccount : BankingTestBase
    {
        public UpdatingABankAccount(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void When_updating_a_bank_account_the_response_should_be_no_content()
        {
            Given
                .BankAccount_has_been_created()
                .GetResult(out BankingStoryData bankAccount);

            When
                .Put($"api/bankaccounts/{bankAccount.BankAccountId}", new BankAccount
                {
                    CustomerName = "New Name"
                });

            Then.Response
                .ShouldBe
                .NoContent();
        }
    }
}