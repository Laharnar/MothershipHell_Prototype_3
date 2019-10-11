public interface IPooling {
    string PoolingGroupTag { get; }

    void OnPooledReady();
    void OnPooledStandby();
}
