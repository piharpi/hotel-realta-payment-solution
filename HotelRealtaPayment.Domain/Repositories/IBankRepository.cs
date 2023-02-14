﻿using HotelRealtaPayment.Domain.Entities;

namespace HotelRealtaPayment.Domain.Repositories
{
    public interface IBankRepository
    {
        IEnumerable<Bank> FindAllBank();
        Task<IEnumerable<Bank>> FindAllBankAsync();
        Bank FindBankById(int bankId);
        T Insert <T>(Bank bank);
        void Edit(Bank bank);
        void Remove(Bank bank);
    }
}
