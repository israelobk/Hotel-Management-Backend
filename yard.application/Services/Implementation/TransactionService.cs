using AutoMapper;
using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using yard.application.Services.Interface;
using yard.domain.Models;
using yard.domain.ViewModels;
using yard.infrastructure.Repositories.Interface;
using yard.infrastructure.Utility;

namespace yard.application.Services.Implementation
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;


        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<ApiResponse> GetAllSuccessfulTransaction(int pageNumber, int pageSize)
        {
            try
            {
                var transactions =  _transactionRepository.GetAllSuccessfulTransaction();
               var transactionPage = await GenericPagination<TransactionVM>.ToPagedListAsync(transactions, pageNumber, pageSize);
                return ApiResponse.Success(transactionPage);
            }
            catch (Exception ex)
            {
                return ApiResponse.Fail(ex.Message, 500);
            }


        }
    }
}
