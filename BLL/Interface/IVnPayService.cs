
using DAL.Entities;
using Microsoft.AspNetCore.Http;
namespace BLL.Interface
{
    public interface IVnPayService
    {
        string CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
        PaymentResponseModel PaymentExecute(IQueryCollection collections);
    }

}