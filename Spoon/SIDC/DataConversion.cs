namespace Spoon.SIDC
{
    class DataConversion
    {
        public DataConversion()
        {

        }

        public static decimal ToDecimal(string textamount)
        {
            decimal amount = 0;
            return decimal.TryParse(textamount, out amount) ? amount : 0;
        }

        public static int ToInt(string textamount)
        {
            int amount = 0;
            return int.TryParse(textamount, out amount) ? amount : 0;
        }
    }
}
