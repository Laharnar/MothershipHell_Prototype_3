public interface IPooling {
    string PoolingGroupTag { get; }

    void OnPooledCreated();
    void OnPooledDestroyed();
}
