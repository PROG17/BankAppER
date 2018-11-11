using BankAppER.Business;
using BankAppER.Models;
using Xunit;


namespace BankAppER.Test
{
    public class BankApp_Tests
    {

        [Fact]
        public void Balance_updated_correctly_when_Deposit()
        {
            // ARRANGE
            decimal startBalance = 1000m;
            decimal deposit = 150.00m; // Insättning

            var bankRepo = new BankRepository();

            var account = new Account()
            {
                Id = 20,
                Balance = startBalance
            };

            var transaction = new Transaction()
            {
                Amount = deposit,
                Balance = startBalance,
                AccountId = 20
            };

            // ACT
            var result = bankRepo.Deposit(transaction, account);
            var expected_balance = 1150.00m;

            // ASSERT
            Assert.True(result);
            Assert.Equal(expected_balance, account.Balance);
        }

        [Fact]
        public void Balance_updated_correctly_when_Withdrawal_with_Amount_which_is_Lower_than_Balance()
        {
            // ARRANGE
            decimal startBalance = 1000m;
            decimal withdrawal = -150.00m; // Uttag

            var bankRepo = new BankRepository();

            var account = new Account()
            {
                Id = 20,
                Balance = startBalance
            };

            var transaction = new Transaction()
            {
                Amount = withdrawal,
                Balance = startBalance,
                AccountId = 20
            };

            // ACT
            var result = bankRepo.Withdrawal(transaction, account);
            var expected_balance = 850.00m;

            // ASSERT
            Assert.True(result);
            Assert.Equal(expected_balance, account.Balance);
        }


        [Fact]
        public void Withdrawal_Fails_when_Amount_is_greater_than_Balance()
        {
            // ARRANGE
            decimal startBalance = 100.00m;
            decimal withdrawal = -150.00m; // Uttag

            var bankRepo = new BankRepository();

            var account = new Account()
            {
                Id = 20,
                Balance = startBalance
            };

            var transaction = new Transaction()
            {
                Amount = withdrawal,
                Balance = startBalance,
                AccountId = 20
            };

            // ACT
            var result = bankRepo.Withdrawal(transaction, account);
            var expected_balance = 100.00m;

            // ASSERT
            Assert.False(result);
            Assert.Equal(expected_balance, account.Balance);
        }





    }
}
