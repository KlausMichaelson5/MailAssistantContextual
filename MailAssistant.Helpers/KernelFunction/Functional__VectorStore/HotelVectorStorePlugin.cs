using MailAssistant.Helpers.Models.Hotel;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Data;
using System.ComponentModel;

namespace MailAssistant.Helpers.KernelFunction
{
    public class HotelVectorStorePlugin
    {
        #pragma warning disable SKEXP0001
        private VectorStoreTextSearch<HotelCustomerVector> _hotelVectors;
        public HotelVectorStorePlugin(VectorStoreTextSearch<HotelCustomerVector> hotelVectors)
        {
            _hotelVectors = hotelVectors;
        }


        [KernelFunction("get_matching_customer")]
        [Description("Gets matching customer if present from email id")]
        public async Task<HotelCustomerVector> GetHotelCustomer(string email)
        {
            //just a poc need to review and change below function
            var query = "get_matching_customer";
            KernelSearchResults<object> customerResult = await _hotelVectors.GetSearchResultsAsync(query);
            HotelCustomerVector customer = new HotelCustomerVector();
            await foreach (HotelCustomerVector result in customerResult.Results)
            { 
                if(result.Email==email) 
                {
                    customer.Email = result.Email;
                    customer.FirstName = result.FirstName;
                    customer.LastName = result.LastName;
                    customer.BookingHistory = result.BookingHistory;
                    customer.HotelCustomerId = result.HotelCustomerId;
                    customer.BookingHistoryEmbedding = result.BookingHistoryEmbedding;
                }
            }
            return customer;

        }

        [KernelFunction("get_all_customers_bookings")]
        [Description("Gets All customers bookings")]
        public async Task<List<HotelCustomerVector>> GetAllBookings()
        {
            //just a poc need to review and change below function
            var query = "get_all_customers_bookings";
            KernelSearchResults<object> customerResult = await _hotelVectors.GetSearchResultsAsync(query);
           List<HotelCustomerVector> allCustomers = [];
            await foreach (HotelCustomerVector result in customerResult.Results)
            {
                HotelCustomerVector customer = new();
                customer.Email = result.Email;
                customer.FirstName = result.FirstName;
                customer.LastName = result.LastName;
                customer.BookingHistory = result.BookingHistory;
                customer.HotelCustomerId = result.HotelCustomerId;
                allCustomers.Add(customer);
                
            }
            return allCustomers;

        }
        ///Add functions here.
        /* 
          1.To add customer,bookings,rooms.
          2.To get customer by email-normal text search. return HotelCustomerclass
          3.To get past bookings of the customer-vector search. return BookingHistoryEmbedding

          based on booking history:price,type of room,no of rooms booked response will be generated---will be told in prompt.        
         
         */
    }
}
