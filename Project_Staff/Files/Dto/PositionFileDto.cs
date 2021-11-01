using System.Xml.Serialization;

namespace Project_Staff
{
    [XmlType(TypeName = "Positions")]
    public class PositionFileDto
    {
        [XmlAttribute("position")]

        public string Title { get; set; }
        public string Devision { get; set; }
        public decimal Salary { get; set; }

        public static PositionFileDto Map(Position position)
        {
            return new PositionFileDto()
            {
                Title = position.Title,
                Devision = position.Devision,
                Salary = position.Salary,
            };
        }
        public static Position Map(PositionFileDto product)
        {
            return new Position(
                product.Title,
                product.Devision,
                product.Salary
            );
        }
    }
}
