import { Fragment, useEffect } from 'react';
import { useParams } from 'react-router-dom';

import HighlightedCustomerApp from '../components/customerapps/HighlightedCustomerApp';
import useHttp from '../hooks/use-http';
import { getSingleCustomerApp } from '../lib/customerapps';
import LoadingSpinner from '../components/UI/LoadingSpinner';

const CustomerAppDetail = () => {
  const params = useParams();

  const { customerAppId } = params;

  const { sendRequest, status, data: loadedCustomerApp, error } = useHttp(
    getSingleCustomerApp,
    true
  );

  useEffect(() => {
    sendRequest(customerAppId);
  }, [sendRequest, customerAppId]);

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

  if (!loadedCustomerApp.appInfo_Name) {
    return <p>No customer app found!</p>;
  }

   // "id": 27,
  // "appInfoId": 1,
  // "appInfo_Name": "RestaurantBag",
  // "appInfo_Description": "حقيبة المطاعم",
  // "customerId": 20,
  // "customer_Name": "مقهى عبد العزيز النجار لتقديم الوجبات( الكناري)",
  // "customer_Phone": "0533417614",
  // "customer_Address": "",
  // "customer_TaxNumber": "300418441400003",
  // "customer_LogoUrl": null,
  // "customer_Description": null,
  // "installDate": "2019-10-17T14:37:04-07:00",
  // "appVersion": " Version 4.1.0 ",
  // "appSerial": "AC55ACE7FBF7F9A5C5A7C",
  // "sysInfo": "Microsoft Windows 7 Ultimate  - 32-bit",
  // "remoteId": null,
  // "price": 0

  return (
    <Fragment>
      <HighlightedCustomerApp appInfo_Name={loadedCustomerApp.appInfo_Name} appSerial={loadedCustomerApp.appSerial} appVersion={loadedCustomerApp.appVersion} customer_Name={loadedCustomerApp.customer_Name} customer_TaxNumber={loadedCustomerApp.customer_TaxNumber} installDate={loadedCustomerApp.installDate} sysInfo={loadedCustomerApp.sysInfo}/>
    </Fragment>
  );
};

export default CustomerAppDetail;
