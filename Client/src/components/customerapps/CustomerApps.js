import { useEffect } from 'react';
import { useParams } from 'react-router-dom';

import classes from './CustomerApps.module.css';
import useHttp from '../../hooks/use-http';
import { getAllCustomerApps } from '../../lib/customerapps';
import LoadingSpinner from '../UI/LoadingSpinner';
import CustomerAppsList from './CustomerAppsList';

const CustomerApps = () => {
  
  const params = useParams();

  const { customerId } = params;
  
  const { sendRequest, status, data: loadedCustomerApps } = useHttp(getAllCustomerApps);

  useEffect(() => {
    sendRequest(customerId);
  }, [customerId, sendRequest]);

  let customerApps;

  if (status === 'pending') {
    customerApps = (
      <div className='centered'>
        <LoadingSpinner />
      </div>
    );
  }

  if (status === 'completed'){
    if(loadedCustomerApps && loadedCustomerApps.length > 0){
      customerApps = <CustomerAppsList customerApps={loadedCustomerApps} />;
    }
    else{
      customerApps = <p className='centered'>No customer apps were added yet!</p>;
    }
  }

  return (
    <section className={classes.customerApps}>     
      {customerApps}
    </section>
  );
};

export default CustomerApps;
