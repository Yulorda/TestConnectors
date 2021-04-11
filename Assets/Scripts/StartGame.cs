using MovingConnector;
using System;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    [SerializeField]
    private int count;

    [SerializeField]
    private Main main;

    [SerializeField]
    private MovingConnectorFactory movingConnectorFactory;

    [SerializeField]
    private ConnectionFactory connectionFactory;

    [SerializeField]
    private FakeConnectorPresenter fakeConnectorPresenter;

    [SerializeField]
    private SelectableColors colors;

    [ContextMenu("Create")]
    public void Create()
    {
        var elements = movingConnectorFactory.Create(count);

        foreach (var element in elements)
        {
            element.SetPosition(GetPointInСircle.GetCoodinates(main.Radius));
        }

        ConnectorsSelectableGroup selectableController = new ConnectorsSelectableGroup(elements);

        var singleClickConnectionController = new SingleClickConnectionController(elements, connectionFactory, selectableController, colors);
        var retentionConnectionFactory = new DragConnectionController(elements, fakeConnectorPresenter, connectionFactory, selectableController, colors);
    }
}