class ConnectionCheck
{
    public int connectedTowers = 1;
    
    public int Connect()
    {
        connectedTowers++;
        return connectedTowers+connectedTowers++ * 10;
    }
}