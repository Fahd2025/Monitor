import { useEffect } from 'react';

import CustomerAppsList from '../components/customerapps/CustomerAppsList';
import LoadingSpinner from '../components/UI/LoadingSpinner';
import NoCustomerAppsFound from '../components/customerapps/NoCustomerAppsFound';
import useHttp from '../hooks/use-http';
import { getAllCustomerApps } from '../lib/customerapps';

const AllCustomerApps = () => {
  const { sendRequest, status, data: loadedCustomerApps, error } = useHttp(
    getAllCustomerApps,
    true
  );

  useEffect(() => {
    sendRequest();
  }, [sendRequest]);

  if (status === 'pending') {
    return (
      <div className='centered'>
        <LoadingSpinner />
      </div>
    );
  }

  if (error) {
    return <p className='centered focused'>{error}</p>;
  }

  if (status === 'completed' && (!loadedCustomerApps || loadedCustomerApps.length === 0)) {
    return <NoCustomerAppsFound />;
  }

  return <CustomerAppsList customerApps={loadedCustomerApps} />;
};

export default AllCustomerApps;
