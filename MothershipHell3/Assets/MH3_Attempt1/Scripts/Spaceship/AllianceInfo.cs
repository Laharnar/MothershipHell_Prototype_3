using UnityEngine;

public static class AllianceInfoHelper {
    public static void InitChildrenToThis(this Stats stats, uint alliance)
    {
        // sets alliances on all children to this.
        Component[] hitComponents = stats.gameObject.GetComponentsInChildren(typeof(IAccessAlliance));
        for (int i = 0; i < hitComponents.Length; i++)
        {
            if (hitComponents[i] is IAccessAlliance)
            {
                IAccessAlliance colInterface = hitComponents[i] as IAccessAlliance;
                colInterface.SetAllianceId(alliance);
            }
        }
    }
}
