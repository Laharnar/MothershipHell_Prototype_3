public class DebugMessage {
    public string Content { get; private set; }
    public bool UseObjTarget { get => ObjTarget != null; }
    public UnityEngine.Object ObjTarget { get; private set; }

    public DebugMessage(string content)
    {
        this.Content = content;
        this.ObjTarget = null;
    }
    public DebugMessage(string content, UnityEngine.Object objTarget)
    {
        this.Content = content;
        this.ObjTarget = objTarget;
    }
}
