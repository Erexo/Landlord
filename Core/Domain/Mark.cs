using System;

namespace Core.Domain
{
    public class Mark
    {
        public int ID { get; private set; }
        public User FromUser { get; private set; }
        public Int16 Value { get; private set; }
        public string Description { get; private set; }
        public DateTime IssueDate { get; private set; }
        
        public House House { get; private set; }

        private Mark()
        {
        }

        private Mark(House house, User FromUser, Int16 Value, string Description)
        {
            House = house;
            this.FromUser = FromUser;
            setValue(Value);
            setDescription(Description);
            IssueDate = DateTime.UtcNow;
        }

        public static Mark Create(House house, User fromUser, Int16 value, string description)
            => new Mark(house, fromUser, value, description);

        private void setValue(Int16 value)
        {
            if (value <= 0 || value > 5)
                throw new Exception("Mark value must be between 1 and 5.");

            Value = value;
        }
        
        private void setDescription(string description)
        {
            if (description.Length <= 0 || description.Length > 300)
                throw new Exception("Invalid description length.");

            Description = description;
        }
    }
}
