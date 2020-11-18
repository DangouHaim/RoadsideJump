public class BoolIntConverter : IConverter
{
    public object Convert(object value)
    {
        if(value is bool)
        {
            bool result = (bool)value;
            
            if(result)
            {
                return 1;
            }
        }
        return 0;
    }
}