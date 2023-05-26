import axios from 'axios'
import React from 'react'

export const locationService = ({from, to}) => {
    return axios({
        method: "get",
        url: `http://localhost:5002/api/distance/${from}/${to}`
    })
    .catch((e)=>{
        console.log(e);
    });
}
