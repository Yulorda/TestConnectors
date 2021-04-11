using System;
using System.Collections.Generic;
using UnityEngine;
using MovingConnector;

public class MovingConnectorFactory : MonoBehaviour
{
    [SerializeField]
    Connector3DObjectPresenterFactory connector3DObjectPresenterFactory;
    [SerializeField]
    CoordinatePresenterFactory сoordinatePresenterFactory;

    public IEnumerable<Connector> Create(int count)
    {
        if (count > 0)
        {
            Connector[] result = new Connector[count];

            for (int i = 0; i < count; i++)
            {
                var mv3doPresenter = connector3DObjectPresenterFactory.Create();
                var coordinatePresenter = сoordinatePresenterFactory.Create();

                Func<Vector3> getPosition = () => mv3doPresenter.transform.position;
                Func<Vector3> getConnectorPosition = () => mv3doPresenter.connectorPoint.transform.position;
                Action<Vector3> setPosition = (x) => mv3doPresenter.transform.position = x;
                result[i] = new Connector(getConnectorPosition, getPosition, setPosition);

                mv3doPresenter.Inject(result[i]);
                coordinatePresenter.Inject(result[i]);
            }

            return result;
        }

        throw new System.Exception("Попытка создать список с отрицательным количеством элементов");
    }
}