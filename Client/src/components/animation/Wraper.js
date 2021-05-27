import {useState, useEffect } from "react";
import classes from './Wraper.module.css';
const Wraper = (props) => {

    const[animate,setAnimate] = useState(false);

    useEffect(()=>{
        const timer = setTimeout(() => {
          setAnimate(true)          
        }, 500);
        return () => {
          clearTimeout(timer);
        }
      },[]);
  return <div className={animate ? classes['rotate-top'] : ''}>{props.children}</div>;
};

export default Wraper;
