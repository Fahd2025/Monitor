import { Fragment } from 'react';
import { useHistory, useLocation } from 'react-router-dom';

import CustomerItem from './CustomerItem';
import classes from './CustomerList.module.css';

const sortCustomers = (customers, ascending) => {
  return customers.sort((customerA, customerB) => {
    if (ascending) {
      return customerA.id > customerB.id ? 1 : -1;
    } else {
      return customerA.id < customerB.id ? 1 : -1;
    }
  });
};

const CustomerList = (props) => {
  const history = useHistory();
  const location = useLocation();

  const queryParams = new URLSearchParams(location.search);

  const isSortingAscending = queryParams.get('sort') === 'asc';

  const sortedCustomers = sortCustomers(props.customers, isSortingAscending);

  const changeSortingHandler = (event) => {
    history.push({
      pathname: location.pathname,
      search: `?sort=${(isSortingAscending ? 'desc' : 'asc')}`
    });

    console.log(event);
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
        {sortedCustomers.map((customer) => (
          <CustomerItem
            key={customer.id}
            id={customer.id}
            name={customer.name}
            taxNumber={customer.taxNumber}
          />
        ))}
      </ul>
    </Fragment>
  );
};

export default CustomerList;
