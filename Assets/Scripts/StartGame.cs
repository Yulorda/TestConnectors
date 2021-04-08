using System.Linq;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField]
    ConnectorFactory connectionPrefabFactory;

    [SerializeField]
    MovingConnector fakeConnector;

    [SerializeField]
    PointerReader fakePointerReader;

    [SerializeField]
    ConnectionPresenterFactory connectionPresenterFactory;


    SingleClickConnectionController singleClickConnectionController;
    RetentionConnectionFactory retentionConnectionFactory;

    [ContextMenu("Create")]
    public void Create()
    {
        var elements = connectionPrefabFactory.Create();
        SelectableGroup selectableController = new SelectableGroup(elements.Select(x=>x.GetSelectable()));

        singleClickConnectionController = new SingleClickConnectionController(elements, connectionPresenterFactory);
        //retentionConnectionFactory = new RetentionConnectionFactory(elements, fakeConnector, fakePointerReader);

    }

    private void OnDestroy()
    {
    }
}