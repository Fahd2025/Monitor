import { Fragment } from 'react';
import { useHistory, useLocation } from 'react-router-dom';

import AppItem from './AppItem';
import classes from './AppList.module.css';

const sortApps = (apps, ascending) => {
  return apps.sort((appA, appB) => {
    if (ascending) {
      return appA.id > appB.id ? 1 : -1;
    } else {
      return appA.id < appB.id ? 1 : -1;
    }
  });
};

const AppList = (props) => {
  const history = useHistory();
  const location = useLocation();

  const queryParams = new URLSearchParams(location.search);

  const isSortingAscending = queryParams.get('sort') === 'asc';

  const sortedApps = sortApps(props.apps, isSortingAscending);

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
        {sortedApps.map((app) => (
          <AppItem
            key={app.id}
            id={app.id}
            name={app.name}
            description={app.description}
          />
        ))}
      </ul>
    </Fragment>
  );
};

export default AppList;
