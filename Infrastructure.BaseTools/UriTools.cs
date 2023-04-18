namespace Infrastructure.BaseTools
{
    public static class UriTools
    {
            public static bool IsValidUri(string uri)
            {
                return Uri.TryCreate(uri, UriKind.Absolute, out _);
            }
    }
}