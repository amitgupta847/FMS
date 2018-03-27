using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace FMS.Business
{
    [DataContract]
    public class Bank
    {
        public Bank()
        {
            Name = Address = IFSC = Phone = Email = Comment = string.Empty;
        }

        [DataMember]
        public int ID { get; set; }

        [DataMember]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [DataMember]
        [Required]
        [MaxLength(250)]
        public string Address { get; set; }

        [DataMember]
        [Required]
        [MaxLength(30)]
        public string IFSC { get; set; }

        [DataMember]
        [MaxLength(20)]
        public string Phone { get; set; }

        [DataMember]
        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; }

        [DataMember]
        [MaxLength(1000)]
        public string Comment { get; set; }
    }


    [DataContract]
    public class Account
    {
        public Account()
        {
            AccountNumber = AccountNumber = OwnerName = Comment = string.Empty;
            IsAccActive = true;
            IsJointlyOwned = false;
            Date_Acc_Open = DateTime.Now;
        }

        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public Bank Bank { get; set; }

        [DataMember]
        public int BankID { get; set; }

        [DataMember]
        [Required]
        [MaxLength(25)]
        public string AccountNumber { get; set; }

        [DataMember]
        [MaxLength(50)]
        public string OwnerName { get; set; }

        [DataMember]
        [MaxLength(50)]
        public string User_Id { get; set; }

        [DataMember]
        [MaxLength(50)]
        public string Pwd_1 { get; set; }

        [DataMember]
        [MaxLength(50)]
        public string Pwd_2 { get; set; }

        [DataMember]
        public DateTime Date_Acc_Open { get; set; }

        [DataMember]
        public bool IsAccActive { get; set; }

        [DataMember]
        public bool IsJointlyOwned { get; set; }

        [DataMember]
        [MaxLength(250)]
        public string Comment { get; set; }

        public string AccountName { get { return string.Format("{0} - {1}", Bank.Name, AccountNumber); } }
    }

    [DataContract]
    public class YearLine
    {
        public YearLine()
        {

        }

        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public int NumberOfDeposits { get; set; }

        [DataMember]
        public decimal YearTotal { get; set; }

        [DataMember]
        public decimal MaturityTotal { get; set; }

        [DataMember]
        public bool IsActive { get; set; }
    }


    [DataContract]
    public class Deposit
    {
        public Deposit()
        {
            DepositAccNumber = Comment = string.Empty;
            MaturityDate = DepositDate = DateTime.Now;
        }

        [DataMember]
        public int ID { get; set; }

        [DataMember]
        [Required]
        [MaxLength(50)]
        public string DepositAccNumber { get; set; }

        [DataMember]
        public YearLine Year { get; set; }

        [DataMember]
        public int YearID { get; set; }

        [DataMember]
        public Account PrimaryLinkedAccount { get; set; }

        [DataMember]
        public int AccountID { get; set; }

        [DataMember]
        [Required]
        public DateTime DepositDate { get; set; }

        [DataMember]
        [Required]
        public float Interest { get; set; }

        [DataMember]
        [Required]
        public DateTime MaturityDate { get; set; }

        [DataMember]
        [Required]
        public decimal InitialDeposit { get; set; }

        [DataMember]
        [Required]
        public decimal MaturityValue { get; set; }


        [DataMember]
        [Required]
        public decimal CurrentValue { get; set; }

        [DataMember]
        [MaxLength(250)]
        public string Comment { get; set; }
    }
}
