namespace AddressProcessing.Address
{
    // Class should be made public as its internal by default.
    class AddressRecord
    {
        public string Address { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
    }
}
