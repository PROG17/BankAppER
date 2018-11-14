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

        [Fact]
        public void Transfer_results_in_correct_Balance_ToAccount()
        {
            // ARRANGE
            decimal fromAccountStartBalance = 100.00m;
            decimal amount = 100.00m;
            int fromAccountId = 1;
            int toAccountId = 2;

            var bankRepo = new BankRepository();

            var fromAccount = new Account()
            {
                Id = 1,
                Balance = fromAccountStartBalance
            };

            var toAccount = new Account()
            {
                Id = 2,
                Balance = 0m
            };

            // ACT
            var result = bankRepo.Transfer(fromAccount, toAccount, amount);

            // ASSERT
            var expected_balance_ToAccount = 100.00m;

            Assert.True(result);
            Assert.Equal(expected_balance_ToAccount, toAccount.Balance);
        }

        [Fact]
        public void Transfer_results_in_correct_Balance_FromAccount()
        {
            // ARRANGE
            decimal fromAccountStartBalance = 100.00m;
            decimal amount = 100.00m;
            int fromAccountId = 1;
            int toAccountId = 2;

            var bankRepo = new BankRepository();

            var fromAccount = new Account()
            {
                Id = 1,
                Balance = fromAccountStartBalance
            };

            var toAccount = new Account()
            {
                Id = 2,
                Balance = 0m
            };

            // ACT
            var result = bankRepo.Transfer(fromAccount, toAccount, amount);

            // ASSERT
            var expected_balance_ToAccount = 0m;

            Assert.True(result);
            Assert.Equal(expected_balance_ToAccount, fromAccount.Balance);
        }

        // Skapa en enhetstest som verifierar att det inte går att överföra
        // mer pengar än det finns saldo på från-kontot.
        [Fact]
        public void Transfer_Fails_when_Amount_is_greater_than_FromAccount_Balance()
        {
            // ARRANGE
            decimal fromAccountStartBalance = 100.00m;
            decimal amount = 200.00m;
            int fromAccountId = 1;
            int toAccountId = 2;

            var bankRepo = new BankRepository();

            var fromAccount = new Account()
            {
                Id = 1,
                Balance = fromAccountStartBalance
            };

            var toAccount = new Account()
            {
                Id = 2,
                Balance = 0m
            };

            // ACT
            var result = bankRepo.Transfer(fromAccount, toAccount, amount);

            // ASSERT

            Assert.False(result);
            
        }





    }
}
