namespace Saleman.Service
{
    public interface ISaleCodeService
    {
        string GenerateCode(long saleId);
        long Decode(string saleCode);
    }
}
