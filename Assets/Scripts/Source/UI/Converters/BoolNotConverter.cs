public class BoolNotConverter : IConverter
{
    public object Convert(object value)
    {
        return !(bool)value;
    }
}