import{z as f,r as o,A as a,B as r}from"./index-a18159e2.js";const g=f("CrewsAdmin",()=>{const c=o(),i=o(),n=o(),l=o(!1);function d(t){n.value=t}function u(t){l.value=t}async function P(t,e,p){try{await a({method:"get",url:r.GETPEOPLE,params:{PostPerPage:t,Page:e,Search:p}}).then(s=>{c.value=s.data})}catch(s){console.log(s)}}async function m(){try{await a({method:"get",url:r.GETPEOPLECOUNT}).then(t=>{console.log(t.data),i.value=t.data})}catch(t){console.log(t)}}async function E(t){try{await a({method:"post",url:r.SAVEPERSON,headers:{"Content-Type":"multipart/form-data"},data:t}).then(e=>{console.log(e.data)})}catch(e){console.log(e)}}async function h(t){try{await a({method:"get",url:r.GETPERSONFROMAPI,params:{Fullname:t}}).then(e=>{n.value=e.data})}catch(e){console.log(e)}}return{SavePerson:E,GetPeople:P,GetPeopleCount:m,PeopleData:c,EditPerson:n,setEditPerson:d,GetPersonFromAPI:h,isEdit:l,setIsEdit:u}}),y=[{route:"/viewcrew",title:"View all crew"},{route:"/addeditperson",title:"Add person"}];export{y as c,g as u};