import{u}from"./moviesadmin-6e261063.js";import{d as p,a as v,r as m,y as h,o as s,c as t,g as d,F as l,s as _,t as f,p as b,l as g,_ as S}from"./index-a18159e2.js";const w=o=>(b("data-v-e4bb9777"),o=o(),g(),o),G=w(()=>d("hr",null,null,-1)),M={class:"grid-container"},x={class:"grid-item"},y={class:"grid-item"},A=p({__name:"MovieSearchView",setup(o){const n=u(),i=v(()=>n.Genres),r=m();return h(async()=>{await n.GetGenres();let e=i.value.length,a=0;if(e!=null&&e%2!=0)for(;e%4!=0;)e++,a++;r.value=a}),(e,a)=>(s(),t("div",null,[G,d("div",M,[(s(!0),t(l,null,_(i.value,c=>(s(),t("div",x,f(c.label),1))),256)),(s(!0),t(l,null,_(r.value,c=>(s(),t("div",y))),256))])]))}});const V=S(A,[["__scopeId","data-v-e4bb9777"]]);export{V as default};