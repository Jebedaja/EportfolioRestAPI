(function(){"use strict";var o={3312:function(o,t,e){var r=e(3751),i=e(641);const a={id:"app"};function n(o,t,e,r,n,l){const s=(0,i.g2)("router-view");return(0,i.uX)(),(0,i.CE)("div",a,[(0,i.bF)(s)])}var l={name:"App"},s=e(6262);const d=(0,s.A)(l,[["render",n]]);var u=d,c=e(5220);const p=o=>((0,i.Qi)("data-v-06673bb2"),o=o(),(0,i.jt)(),o),f={class:"home-container"},m={class:"welcome-box"},h=p((()=>(0,i.Lk)("h1",null,"Welcome to E-Portfolio",-1))),k={class:"links"};function b(o,t,e,r,a,n){const l=(0,i.g2)("router-link");return(0,i.uX)(),(0,i.CE)("div",f,[(0,i.Lk)("div",m,[h,(0,i.Lk)("div",k,[(0,i.bF)(l,{to:"/login",class:"link-button"},{default:(0,i.k6)((()=>[(0,i.eW)("Login")])),_:1}),(0,i.bF)(l,{to:"/register",class:"link-button"},{default:(0,i.k6)((()=>[(0,i.eW)("Register")])),_:1})])])])}var g={name:"HomeView"};const v=(0,s.A)(g,[["render",b],["__scopeId","data-v-06673bb2"]]);var L=v;const y=o=>((0,i.Qi)("data-v-029f3b08"),o=o(),(0,i.jt)(),o),w={class:"auth-container"},P={class:"auth-box"},E=y((()=>(0,i.Lk)("h2",null,"Login",-1))),$={class:"form-group"},A=y((()=>(0,i.Lk)("label",null,"Email:",-1))),C={class:"form-group"},_=y((()=>(0,i.Lk)("label",null,"Password:",-1))),T={class:"form-group checkbox"},M=y((()=>(0,i.Lk)("button",{type:"submit",class:"submit-button"},"Login",-1)));function x(o,t,e,a,n,l){return(0,i.uX)(),(0,i.CE)("div",w,[(0,i.Lk)("div",P,[E,(0,i.Lk)("form",{onSubmit:t[3]||(t[3]=(0,r.D$)(((...o)=>l.login&&l.login(...o)),["prevent"]))},[(0,i.Lk)("div",$,[A,(0,i.bo)((0,i.Lk)("input",{type:"email","onUpdate:modelValue":t[0]||(t[0]=o=>n.email=o),required:""},null,512),[[r.Jo,n.email]])]),(0,i.Lk)("div",C,[_,(0,i.bo)((0,i.Lk)("input",{type:"password","onUpdate:modelValue":t[1]||(t[1]=o=>n.password=o),required:""},null,512),[[r.Jo,n.password]])]),(0,i.Lk)("div",T,[(0,i.Lk)("label",null,[(0,i.bo)((0,i.Lk)("input",{type:"checkbox","onUpdate:modelValue":t[2]||(t[2]=o=>n.rememberMe=o)},null,512),[[r.lH,n.rememberMe]]),(0,i.eW)(" Remember Me ")])]),M],32)])])}e(4114);var S=e(4335),j={data(){return{email:"",password:"",rememberMe:!1}},methods:{async login(){try{const o=await S.A.post("/api/Account/Login",{email:this.email,password:this.password,rememberMe:this.rememberMe});localStorage.setItem("token",o.data.token),this.$router.push({name:"portfolio"})}catch(o){console.error("Login failed",o)}}}};const F=(0,s.A)(j,[["render",x],["__scopeId","data-v-029f3b08"]]);var N=F;const O=o=>((0,i.Qi)("data-v-6ac109a3"),o=o(),(0,i.jt)(),o),I={class:"auth-container"},U={class:"auth-box"},V=O((()=>(0,i.Lk)("h2",null,"Register",-1))),X={class:"form-group"},J=O((()=>(0,i.Lk)("label",null,"Email:",-1))),q={class:"form-group"},D=O((()=>(0,i.Lk)("label",null,"Password:",-1))),Q={class:"form-group"},R=O((()=>(0,i.Lk)("label",null,"Confirm Password:",-1))),W=O((()=>(0,i.Lk)("button",{type:"submit",class:"submit-button"},"Register",-1)));function z(o,t,e,a,n,l){return(0,i.uX)(),(0,i.CE)("div",I,[(0,i.Lk)("div",U,[V,(0,i.Lk)("form",{onSubmit:t[3]||(t[3]=(0,r.D$)(((...o)=>l.register&&l.register(...o)),["prevent"]))},[(0,i.Lk)("div",X,[J,(0,i.bo)((0,i.Lk)("input",{type:"email","onUpdate:modelValue":t[0]||(t[0]=o=>n.email=o),required:""},null,512),[[r.Jo,n.email]])]),(0,i.Lk)("div",q,[D,(0,i.bo)((0,i.Lk)("input",{type:"password","onUpdate:modelValue":t[1]||(t[1]=o=>n.password=o),required:""},null,512),[[r.Jo,n.password]])]),(0,i.Lk)("div",Q,[R,(0,i.bo)((0,i.Lk)("input",{type:"password","onUpdate:modelValue":t[2]||(t[2]=o=>n.confirmPassword=o),required:""},null,512),[[r.Jo,n.confirmPassword]])]),W],32)])])}var B={data(){return{email:"",password:"",confirmPassword:""}},methods:{async register(){if(this.password===this.confirmPassword)try{const o=await S.A.post("/api/Account/Register",{email:this.email,password:this.password,confirmPassword:this.confirmPassword});console.log("Registration successful:",o.data),this.$router.push({name:"login"})}catch(o){console.error("Registration failed:",o.response?o.response.data:o.message)}else console.error("Passwords do not match")}}};const H=(0,s.A)(B,[["render",z],["__scopeId","data-v-6ac109a3"]]);var Y=H,K=e(33);const G=o=>((0,i.Qi)("data-v-6732d84f"),o=o(),(0,i.jt)(),o),Z={class:"portfolio-container"},oo={class:"portfolio-box"},to=G((()=>(0,i.Lk)("h2",null,"Portfolio",-1))),eo=G((()=>(0,i.Lk)("button",{class:"add-button"},"Add New Portfolio",-1))),ro={key:0},io=["href"],ao=["onClick"],no={key:1},lo=G((()=>(0,i.Lk)("p",null,"No portfolio content available.",-1))),so=[lo],uo={key:2,class:"error-message"};function co(o,t,e,r,a,n){const l=(0,i.g2)("router-link");return(0,i.uX)(),(0,i.CE)("div",Z,[(0,i.Lk)("div",oo,[to,(0,i.bF)(l,{to:{name:"add-portfolio"}},{default:(0,i.k6)((()=>[eo])),_:1}),a.portfolios.length>0?((0,i.uX)(),(0,i.CE)("div",ro,[((0,i.uX)(!0),(0,i.CE)(i.FK,null,(0,i.pI)(a.portfolios,(o=>((0,i.uX)(),(0,i.CE)("div",{key:o.id,class:"portfolio-item"},[(0,i.Lk)("h3",null,(0,K.v_)(o.portfolioName),1),(0,i.Lk)("p",null,(0,K.v_)(o.description),1),(0,i.Lk)("a",{href:o.youTubeLink,target:"_blank"},"Watch on YouTube",8,io),(0,i.Lk)("button",{onClick:t=>n.deletePortfolio(o.id),class:"delete-button"},"Delete",8,ao),(0,i.bF)(l,{to:{name:"edit-portfolio",params:{id:o.id}},class:"edit-button"},{default:(0,i.k6)((()=>[(0,i.eW)("Edit")])),_:2},1032,["to"])])))),128))])):((0,i.uX)(),(0,i.CE)("div",no,so)),a.error?((0,i.uX)(),(0,i.CE)("div",uo,[(0,i.Lk)("p",null,(0,K.v_)(a.error),1)])):(0,i.Q3)("",!0)])])}var po={data(){return{portfolios:[],error:null}},mounted(){this.fetchPortfolioContent()},methods:{async fetchPortfolioContent(){try{const o=await fetch("/api/Account/Portfolio",{headers:{Authorization:`Bearer ${localStorage.getItem("token")}`}});if(o.ok)this.portfolios=await o.json();else{const t=await o.text();this.error=`Failed to fetch portfolio content: ${o.status} ${t}`,console.error("Failed to fetch portfolio content",o.status,t)}}catch(o){this.error=`Error fetching portfolio content: ${o.message}`,console.error("Error fetching portfolio content:",o)}},async deletePortfolio(o){try{const t=await fetch(`/api/Account/Portfolio/${o}`,{method:"DELETE",headers:{Authorization:`Bearer ${localStorage.getItem("token")}`}});if(t.ok)this.portfolios=this.portfolios.filter((t=>t.id!==o));else{const e=await t.text();this.error=`Failed to delete portfolio with id ${o}: ${t.status} ${e}`,console.error(`Failed to delete portfolio with id ${o}`,t.status,e)}}catch(t){this.error=`Error deleting portfolio with id ${o}: ${t.message}`,console.error(`Error deleting portfolio with id ${o}:`,t)}}}};const fo=(0,s.A)(po,[["render",co],["__scopeId","data-v-6732d84f"]]);var mo=fo;const ho=o=>((0,i.Qi)("data-v-c731f1a0"),o=o(),(0,i.jt)(),o),ko={class:"add-portfolio"},bo=ho((()=>(0,i.Lk)("h2",null,"Add Portfolio",-1))),go={class:"form-group"},vo=ho((()=>(0,i.Lk)("label",{for:"portfolioName"},"Portfolio Name:",-1))),Lo={class:"form-group"},yo=ho((()=>(0,i.Lk)("label",{for:"description"},"Description:",-1))),wo={class:"form-group"},Po=ho((()=>(0,i.Lk)("label",{for:"youTubeLink"},"YouTube Link:",-1))),Eo=ho((()=>(0,i.Lk)("button",{type:"submit"},"Add Portfolio",-1))),$o={key:0,class:"error-message"};function Ao(o,t,e,a,n,l){return(0,i.uX)(),(0,i.CE)("div",ko,[bo,(0,i.Lk)("form",{onSubmit:t[3]||(t[3]=(0,r.D$)(((...o)=>l.addPortfolio&&l.addPortfolio(...o)),["prevent"]))},[(0,i.Lk)("div",go,[vo,(0,i.bo)((0,i.Lk)("input",{type:"text",id:"portfolioName","onUpdate:modelValue":t[0]||(t[0]=o=>n.portfolio.portfolioName=o),required:""},null,512),[[r.Jo,n.portfolio.portfolioName]])]),(0,i.Lk)("div",Lo,[yo,(0,i.bo)((0,i.Lk)("textarea",{id:"description","onUpdate:modelValue":t[1]||(t[1]=o=>n.portfolio.description=o),required:""},null,512),[[r.Jo,n.portfolio.description]])]),(0,i.Lk)("div",wo,[Po,(0,i.bo)((0,i.Lk)("input",{type:"text",id:"youTubeLink","onUpdate:modelValue":t[2]||(t[2]=o=>n.portfolio.youTubeLink=o),required:""},null,512),[[r.Jo,n.portfolio.youTubeLink]])]),Eo,n.errorMessage?((0,i.uX)(),(0,i.CE)("div",$o,(0,K.v_)(n.errorMessage),1)):(0,i.Q3)("",!0)],32)])}var Co={data(){return{portfolio:{portfolioName:"",description:"",youTubeLink:"",dateAdded:new Date},errorMessage:""}},methods:{async addPortfolio(){try{const o=await fetch("/api/Account/Portfolio",{method:"POST",headers:{"Content-Type":"application/json",Authorization:`Bearer ${localStorage.getItem("token")}`},body:JSON.stringify(this.portfolio)});o.ok?this.$router.push({name:"portfolio"}):(console.error("Failed to add portfolio"),this.errorMessage="Failed to add portfolio")}catch(o){console.error("Error adding portfolio:",o),this.errorMessage=`Error adding portfolio: ${o.message}`}}}};const _o=(0,s.A)(Co,[["render",Ao],["__scopeId","data-v-c731f1a0"]]);var To=_o;const Mo=o=>((0,i.Qi)("data-v-ccd93e4a"),o=o(),(0,i.jt)(),o),xo={class:"edit-portfolio"},So=Mo((()=>(0,i.Lk)("h2",null,"Edit Portfolio",-1))),jo={class:"form-group"},Fo=Mo((()=>(0,i.Lk)("label",{for:"portfolioName"},"Portfolio Name:",-1))),No={class:"form-group"},Oo=Mo((()=>(0,i.Lk)("label",{for:"description"},"Description:",-1))),Io={class:"form-group"},Uo=Mo((()=>(0,i.Lk)("label",{for:"youTubeLink"},"YouTube Link:",-1))),Vo=Mo((()=>(0,i.Lk)("button",{type:"submit"},"Update Portfolio",-1))),Xo={key:0,class:"error-message"};function Jo(o,t,e,a,n,l){return(0,i.uX)(),(0,i.CE)("div",xo,[So,(0,i.Lk)("form",{onSubmit:t[3]||(t[3]=(0,r.D$)(((...o)=>l.updatePortfolio&&l.updatePortfolio(...o)),["prevent"]))},[(0,i.Lk)("div",jo,[Fo,(0,i.bo)((0,i.Lk)("input",{type:"text",id:"portfolioName","onUpdate:modelValue":t[0]||(t[0]=o=>n.portfolio.portfolioName=o),required:""},null,512),[[r.Jo,n.portfolio.portfolioName]])]),(0,i.Lk)("div",No,[Oo,(0,i.bo)((0,i.Lk)("textarea",{id:"description","onUpdate:modelValue":t[1]||(t[1]=o=>n.portfolio.description=o),required:""},null,512),[[r.Jo,n.portfolio.description]])]),(0,i.Lk)("div",Io,[Uo,(0,i.bo)((0,i.Lk)("input",{type:"text",id:"youTubeLink","onUpdate:modelValue":t[2]||(t[2]=o=>n.portfolio.youTubeLink=o),required:""},null,512),[[r.Jo,n.portfolio.youTubeLink]])]),Vo,n.errorMessage?((0,i.uX)(),(0,i.CE)("div",Xo,(0,K.v_)(n.errorMessage),1)):(0,i.Q3)("",!0)],32)])}var qo={data(){return{portfolio:{id:null,portfolioName:"",description:"",youTubeLink:""},errorMessage:""}},async created(){const o=this.$route.params.id;await this.fetchPortfolio(o)},methods:{async fetchPortfolio(o){try{const t=await fetch(`/api/Account/Portfolio/${o}`,{headers:{Authorization:`Bearer ${localStorage.getItem("token")}`}});t.ok?this.portfolio=await t.json():(console.error(`Failed to fetch portfolio with id ${o}`),this.errorMessage=`Failed to fetch portfolio with id ${o}`)}catch(t){console.error(`Error fetching portfolio with id ${o}:`,t),this.errorMessage=`Error fetching portfolio with id ${o}: ${t.message}`}},async updatePortfolio(){try{const o=await fetch(`/api/Account/Portfolio/${this.portfolio.id}`,{method:"PUT",headers:{"Content-Type":"application/json",Authorization:`Bearer ${localStorage.getItem("token")}`},body:JSON.stringify(this.portfolio)});o.ok?this.$router.push({name:"portfolio"}):(console.error(`Failed to update portfolio with id ${this.portfolio.id}`),this.errorMessage=`Failed to update portfolio with id ${this.portfolio.id}`)}catch(o){console.error(`Error updating portfolio with id ${this.portfolio.id}:`,o),this.errorMessage=`Error updating portfolio with id ${this.portfolio.id}: ${o.message}`}}}};const Do=(0,s.A)(qo,[["render",Jo],["__scopeId","data-v-ccd93e4a"]]);var Qo=Do;const Ro=(o,t,e)=>{const r=localStorage.getItem("token");console.log("Checking auth, token:",r),r?e():e({name:"login"})},Wo=[{path:"/",name:"HomeView",component:L},{path:"/login",name:"login",component:N},{path:"/register",name:"register",component:Y},{path:"/portfolio",name:"portfolio",component:mo,beforeEnter:Ro},{path:"/add-portfolio",name:"add-portfolio",component:To,beforeEnter:Ro},{path:"/edit-portfolio/:id",name:"edit-portfolio",component:Qo,props:!0,beforeEnter:Ro}],zo=(0,c.aE)({history:(0,c.LA)("/"),routes:Wo});var Bo=zo;(0,r.Ef)(u).use(Bo).mount("#app")}},t={};function e(r){var i=t[r];if(void 0!==i)return i.exports;var a=t[r]={exports:{}};return o[r].call(a.exports,a,a.exports,e),a.exports}e.m=o,function(){var o=[];e.O=function(t,r,i,a){if(!r){var n=1/0;for(u=0;u<o.length;u++){r=o[u][0],i=o[u][1],a=o[u][2];for(var l=!0,s=0;s<r.length;s++)(!1&a||n>=a)&&Object.keys(e.O).every((function(o){return e.O[o](r[s])}))?r.splice(s--,1):(l=!1,a<n&&(n=a));if(l){o.splice(u--,1);var d=i();void 0!==d&&(t=d)}}return t}a=a||0;for(var u=o.length;u>0&&o[u-1][2]>a;u--)o[u]=o[u-1];o[u]=[r,i,a]}}(),function(){e.d=function(o,t){for(var r in t)e.o(t,r)&&!e.o(o,r)&&Object.defineProperty(o,r,{enumerable:!0,get:t[r]})}}(),function(){e.g=function(){if("object"===typeof globalThis)return globalThis;try{return this||new Function("return this")()}catch(o){if("object"===typeof window)return window}}()}(),function(){e.o=function(o,t){return Object.prototype.hasOwnProperty.call(o,t)}}(),function(){e.r=function(o){"undefined"!==typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(o,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(o,"__esModule",{value:!0})}}(),function(){var o={524:0};e.O.j=function(t){return 0===o[t]};var t=function(t,r){var i,a,n=r[0],l=r[1],s=r[2],d=0;if(n.some((function(t){return 0!==o[t]}))){for(i in l)e.o(l,i)&&(e.m[i]=l[i]);if(s)var u=s(e)}for(t&&t(r);d<n.length;d++)a=n[d],e.o(o,a)&&o[a]&&o[a][0](),o[a]=0;return e.O(u)},r=self["webpackChunkfrontend"]=self["webpackChunkfrontend"]||[];r.forEach(t.bind(null,0)),r.push=t.bind(null,r.push.bind(r))}();var r=e.O(void 0,[504],(function(){return e(3312)}));r=e.O(r)})();
//# sourceMappingURL=app.2c8e846d.js.map