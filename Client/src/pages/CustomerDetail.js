import { Fragment, useEffect } from 'react';
import { useParams, Route, Link, useRouteMatch } from 'react-router-dom';

import HighlightedCustomer from '../components/customers/HighlightedCustomer';
import CustomerApps from '../components/customerapps/CustomerApps';
import useHttp from '../hooks/use-http';
import { getSingleCustomer } from '../lib/customers';
import LoadingSpinner from '../components/UI/LoadingSpinner';

const CustomerDetail = () => {
  const match = useRouteMatch();
  const params = useParams();

  const { customerId } = params;

  const { sendRequest, status, data: loadedCustomer, error } = useHttp(
    getSingleCustomer,
    true
  );

  useEffect(() => {
    sendRequest(customerId);
  }, [sendRequest, customerId]);

  if (status === 'pending') {
    return (
      <div className='centered'>
        <LoadingSpinner />
      </div>
    );
  }

  if (error) {
    return <p className='centered'>{error}</p>;
  }

  if (!loadedCustomer.name) {
    return <p>No customer found!</p>;
  }

  return (
    <Fragment>
      <HighlightedCustomer name={loadedCustomer.name} phone={loadedCustomer.phone} address={loadedCustomer.address} taxNumber={loadedCustomer.taxNumber}/>     
      <Route path={match.path} exact>
        <div className='centered'>
          <Link className='btn--flat' to={`${match.url}/apps`}>
            Load cutomer apps
          </Link>
        </div>
      </Route>
      <Route path={`${match.path}/apps`}>
        <CustomerApps />
      </Route>
    </Fragment>
  );
};

export default CustomerDetail;
