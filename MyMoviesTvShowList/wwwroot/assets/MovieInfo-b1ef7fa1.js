import{d as y,M as I,N as S,r as i,O as k,a as C,n as D,P as L,w as x,o as h,c as f,g as e,t as r,E,F as O,s as B,h as N,Q as R,p as A,l as F,_ as G}from"./index-3be30c5c.js";const o=l=>(A("data-v-8f64f220"),l=l(),F(),l),P={id:"left-side"},T={id:"movie-img"},U=["src"],V={id:"center-side"},W={id:"synopsis"},j=o(()=>e("h4",null,"Synopsis",-1)),z={id:"right-side"},Q={id:"list-menu"},$={class:"selected-value"},q=o(()=>e("span",{class:"arrow"},null,-1)),H={class:"select-dropdown",role:"listbox",id:"select-dropdown"},J=["value","id"],K=["for"],X=o(()=>e("li",null,"Rate",-1)),Y=o(()=>e("li",null,null,-1)),Z=o(()=>e("li",null,null,-1)),ee=y({__name:"MovieInfo",setup(l){const m=I(),c=S(),g=Number(m.params.id),d=i(new k),s=i(!1),u=i(),a=i(),b=C(()=>a.value||{label:"Add to list"}),M=[{value:1,label:"Watching"},{value:2,label:"Plan To Watch"},{value:3,label:"Completed"}],v=n=>{u.value&&!u.value.contains(n.target)&&s.value==!0&&(s.value=!1)};D(async()=>{document.addEventListener("click",v),await c.GetMovieInfo(g),d.value=c.GetMovie(),c.resetMovieInfo()}),L(()=>{document.removeEventListener("click",v)}),x(a,n=>{console.log(n),_()});const _=()=>{s.value=!s.value};return(n,p)=>(h(),f("section",null,[e("div",P,[e("div",T,[e("img",{src:`data:image/jpeg;base64,${d.value.MovieImageData}`},null,8,U)])]),e("div",V,[e("div",W,[j,e("p",null,r(d.value.Synopsis),1)])]),e("div",z,[e("ul",Q,[e("li",{id:"watched-select",ref_key:"statusDropdown",ref:u},[e("div",{class:E(["custom-select",s.value?"active":null])},[e("button",{class:"select-button",onClick:_},[e("span",$,r(b.value.label),1),q]),e("ul",H,[(h(),f(O,null,B(M,t=>e("li",{role:"option",key:t.value,onClick:_},[N(e("input",{type:"radio",value:t,id:t.label,"onUpdate:modelValue":p[0]||(p[0]=w=>a.value=w)},null,8,J),[[R,a.value]]),e("label",{for:t.label},r(t.label),9,K)])),64))])],2)],512),X,Y,Z])])]))}});const te=G(ee,[["__scopeId","data-v-8f64f220"]]);export{te as default};