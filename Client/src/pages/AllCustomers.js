import { useEffect } from 'react';

import CustomerList from '../components/customers/CustomerList';
import LoadingSpinner from '../components/UI/LoadingSpinner';
import NoCustomersFound from '../components/customers/NoCustomersFound';
import useHttp from '../hooks/use-http';
import { getAllCustomers } from '../lib/customers';

const AllCustomers = () => {
  const { sendRequest, status, data: loadedCustomers, error } = useHttp(
    getAllCustomers,
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

  if (status === 'completed' && (!loadedCustomers || loadedCustomers.length === 0)) {
    return <NoCustomersFound />;
  }

  return <CustomerList customers={loadedCustomers} />;
};

export default AllCustomers;
