using BankAppER.Business;
using BankAppER.Models;
using System;
using System.Linq;
using Xunit;
using Moq;


namespace BankAppER.Test
{
    public class BankApp_Tests
    {

        private BankRepository _repository;
        private Mock<DbSet<SomeClass>> _mockSomeClass;

        
        public void TestSetUp()
        {
            _mockSomeClass = new Mock<DbSet<SomeClass>>();

            var mockContext = new Mock<IApplicationDbContext>();
            mockContext.SetupGet(c => c.SomeClass).Returns(_mockSomeClass.Object);

            _repository = new SomeClassRepository(mockContext.Object);
        }



        [Fact]
        public void Balance_updated_correctly_when_Withdrawal_with_Amount_which_is_Lower_than_Balance()
        {
            //arrange
            decimal startBalance = 1000m;
            decimal withdrawal = 150.00m; // Uttag

            var account = new Account();
            var lastId = _repo.GetAccounts().OrderByDescending(c => c.Id).FirstOrDefault().Id;

            account.Id = lastId + 1;
            account.CustomerId = 1;
            account.Balance = startBalance;

            var trans = new Transaction();

            trans.Amount = withdrawal;
            trans.Balance = startBalance;

            // ACT
            var result = _repo.Withdrawal(trans);

            //ASSERT
            Assert.True(result);
            Assert.Equal(850, account.Balance);
        }



        [Fact]
        public void Withdrawal_Fails_when_Amount_is_greater_than_Balance()
        {
            // Arrange
            decimal startBalance = 100.00m;
            decimal withdrawal =  150.00m; // Uttag

            var account = _repo.GetAccountById(1);
            account.Balance = startBalance;

            var trans = new Transaction();
            trans.Date = DateTime.Now;
            trans.Amount = withdrawal;
            trans.Balance = startBalance;
            trans.AccountId = account.Id;

            // ACT
            var result = _repo.Withdrawal(trans);

            //ASSERT
            Assert.False(result);
        }

    }
}
