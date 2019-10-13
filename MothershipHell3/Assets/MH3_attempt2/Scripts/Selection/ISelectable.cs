public interface ISelectable {
    /*void OnSelectedSingle();
    void OnSelectedDragArea();
    void OnDeselectSingle();
    void OnDeselectArea();*/
    void OnSelected();
    void OnDeselected();
    BasicMono SelectionSource { get; }
    STANDSelectableMono SelectionSource2 { get; }
}
