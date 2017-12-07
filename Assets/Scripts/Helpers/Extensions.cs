namespace CardProject.Helpers
{
    public static class Extensions
    {
        public static string ToStringWithPlus(this int value)
        {
            return value.ToString("+#;-#;0");
        }
    }
}