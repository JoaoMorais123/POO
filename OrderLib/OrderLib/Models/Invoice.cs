using System;

namespace OrderLib.Models
{
    /// <summary>
    /// Represents an invoice with billing details.
    /// </summary>
    public class Invoice : Entity
    {
        private string _invoiceNumber;
        private DateTime _invoiceDate;
        private decimal _totalAmount;
        private DateTime _dueDate;

        public string InvoiceNumber => _invoiceNumber;
        public DateTime InvoiceDate => _invoiceDate;
        public decimal TotalAmount => _totalAmount;
        public DateTime DueDate => _dueDate;

        public Invoice(int id, string invoiceNumber, decimal totalAmount)
            : base(id)
        {
            _invoiceNumber = invoiceNumber;
            _invoiceDate = DateTime.Now;
            _totalAmount = totalAmount;
            _dueDate = _invoiceDate.AddDays(30); // Default due date is 30 days after invoice date
        }

        /// <summary>
        /// Processes the payment and updates invoice status.
        /// </summary>
        /// <returns>True if payment is processed successfully; otherwise, false.</returns>
        public bool ProcessPayment()
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