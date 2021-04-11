using MovingConnector;
using UnityEngine;

namespace MovingConnector
{
    public class ConnectionFactory : MonoBehaviour
    {
        [SerializeField]
        ConnectionLinePresenterFactory connectionPresenterFactory;

        public Connection Create(params Connector[] connectors)
        {
            var result = new Connection(connectors);
            var presenter = connectionPresenterFactory.Create();
            presenter.Inject(result);
            return result;
        }
    }
}