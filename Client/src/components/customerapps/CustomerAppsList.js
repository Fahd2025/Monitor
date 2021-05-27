import { Fragment } from 'react';
import { useHistory, useLocation } from 'react-router-dom';

import CustomerAppItem from './CustomerAppItem';
import classes from './CustomerAppsList.module.css';

const sortCustomerApps = (customerApps, ascending) => {
  return customerApps.sort((customerAppA, customerAppB) => {
    if (ascending) {
      return customerAppA.id > customerAppB.id ? 1 : -1;
    } else {
      return customerAppA.id < customerAppB.id ? 1 : -1;
    }
  });
};

const CustomerAppsList = (props) => {
  const history = useHistory();
  const location = useLocation();

  const queryParams = new URLSearchParams(location.search);

  const isSortingAscending = queryParams.get('sort') === 'asc';

  const sortedCustomerApps = sortCustomerApps(props.customerApps, isSortingAscending);

  const changeSortingHandler = (event) => {
    history.push({
      pathname: location.pathname,
      search: `?sort=${(isSortingAscending ? 'desc' : 'asc')}`
    });

    if(isSortingAscending){
      event.target.className = '';
    }
    else{
      event.target.className = classes['scale-up-center'];
    }
  };

  return (
    <Fragment>
      <div className={classes.sorting}>
        <button onClick={changeSortingHandler}>
          Sort {isSortingAscending ? 'Descending' : 'Ascending'}
        </button>
      </div>
      <ul className={classes.list}>
        {sortedCustomerApps.map((customerApp) => (
          <CustomerAppItem
            key={customerApp.id}
            id={customerApp.id}
            appInfo_Name={customerApp.appInfo_Name}
            appSerial={customerApp.appSerial}
          />
        ))}
      </ul>
    </Fragment>
  );
};

export default CustomerAppsList;
