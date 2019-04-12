namespace BlockDudes.Services
{
    interface IHashService
    {
        byte[] GetHash(byte[] input);
    }
}
