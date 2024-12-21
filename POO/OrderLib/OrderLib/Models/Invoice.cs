using System;

namespace OrderLib.Models
{
    /// <summary>
    /// Represents an invoice with billing details.
    /// </summary>
    public class Invoice : Entity
    {
        private string _processPayment;
        private DateTime _invoiceDate;
        private double _totalAmount;
        
        private int _orderId;
        
        public DateTime InvoiceDate => _invoiceDate;
        public double TotalAmount => _totalAmount;
        
        public int IDOrderLine => _orderId;
        public string ProcessPayment => _processPayment;

        public Invoice(int id, double totalAmount, string processPayment)
            : base(id)
        {
            _invoiceDate = DateTime.Now;
            _totalAmount = totalAmount;
            _processPayment = processPayment;
        }

        /// <summary>
        /// Processes the payment and updates invoice status.
        /// </summary>
        /// <returns>True if payment is processed successfully; otherwise, false.</returns>
        public bool _ProcessPayment()
        {
            // Logic for processing payment
            return true;
        }

        public override string DisplayInfo()
        {
            throw new NotImplementedException();
        }
    }

}