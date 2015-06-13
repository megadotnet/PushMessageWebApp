﻿/*!@license
* Infragistics.Web.MobileUI Rating 14.2.20142.1018
*
* Copyright (c) 2011-2014 Infragistics Inc.
*
* http://www.infragistics.com/
*
* Depends on: 
 *  jquery-1.7.2.js
 *  jquery.mobile-1.2.0.js
*/
(function($){var _aNull=function(v){return v===null||v===undefined||typeof v==="number"&&isNaN(v)};$.widget("mobile.igRating",$.mobile.widget,{options:{value:null,voteCount:5,voteWidth:0,voteHeight:0,readOnly:false,inputName:null},css:{normal:"ui-igrating ui-state-default ui-widget-content",selected:"ui-igrating-selected ui-state-highlight",vote:"ui-igrating-vote ui-icon ui-icon-star",voteSelected:"ui-igrating-voteselected",voteReadOnly:"ui-igrating-votereadonly",voteReadOnlySelected:"ui-igrating-votereadonlyselected",voteHover:"ui-igrating-votehover",hovered:"ui-igrating-hovered"},events:{hoverChange:null,valueChange:null},_create:function(){var count,v,cont,elem,sto={fontSize:"1px",width:"100%",height:"100%",position:"relative",overflow:"hidden"},o=this.options,elem0=this.element,me=this,css=this.css;count=this._count(o);v=elem0[0].style;me._old={width:v.width,height:v.height,html:elem0[0].innerHTML};me._swap=elem0.css("direction")==="rtl";elem=me._elem=$("<div/>").css(sto).addClass(css.normal).appendTo(elem0);cont=$("<div/>").css(sto).appendTo(elem);me._doVotes(o,cont);me._val=me._toNum(o.value);me._doVal(me._val);me._doVal(me._val,false,true);me._skip=0;elem.bind(me._evts={click:function(e){if(--me._skip<=0){me._doEvt(e,1)}},mouseup:function(e){if(--me._skip<=0){me._doEvt(e,1);me._skip=2}},mousemove:function(e){if(!$.support.touch){me._doEvt(e,2)}},vmouseout:function(e){me._lastHov=null;me._doEvt(e,3)},swipeleft:function(e){me._skip=2;me._doEvt(e,!me._swap?4:5)},swiperight:function(e){me._skip=2;me._doEvt(e,me._swap?4:5)}})},_toNum:function(v){if(!v){return 0}return isNaN(v)||v<0?0:v},_count:function(o){o=parseInt(o.voteCount,10);return isNaN(o)?5:Math.max(o,1)},_doVotes:function(o,cont){var sel,hov,cssV,height,width,div,span,cssi,val=cont,count=this._count(o),sto={width:"100%",height:"100%"},abs={left:"0px",top:"0px",position:"absolute",overflow:"hidden",border:"none",background:"none",whiteSpace:"nowrap"},i=-1,me=this,css=this.css,elem=this._elem,elem0=this.element;if(!cont){cont=me._div.parent();me._div.remove();me._hov.remove();me._sel.remove();me._selSwap=me._hovSwap=null}div=me._div=$("<div/>").addClass(css.vote).css(abs).appendTo(cont);if(o.readOnly){div.addClass(css.voteReadOnly)}height=parseInt(o.voteHeight,10);width=parseInt(o.voteWidth,10);if(isNaN(height)||height<2){height=div.css("height");height=!height||height.indexOf("px")<1?16:parseInt(height,10)}if(isNaN(width)||width<2){width=div.css("width");width=!width||width.indexOf("px")<1?16:parseInt(width,10)}div.removeClass(css.vote).removeClass(css.voteReadOnly);sel=me._sel=$("<div/>").addClass(css.selected).css(sto).css(abs).appendTo(cont);if(!$.support.touch){hov=me._hov=$("<div/>").addClass(css.hovered).css(sto).css(abs).css("visibility","hidden").appendTo(cont)}if(me._swap){if(!$.support.touch){me._hovSwap=hov=$("<div/>").css(sto).appendTo(me._hov)}me._selSwap=sel=$("<div/>").css(sto).appendTo(me._sel)}cssV={display:"inline-block",width:width,height:height,textIndent:"0px",overflow:"visible"};while(++i<count){span=$("<span />").addClass(css.vote).css(cssV).appendTo(div);if(o.readOnly){span.addClass(css.voteReadOnly)}cssi=o.cssVotes?o.cssVotes[i]:null;if(cssi&&cssi[0]){span.addClass(cssi[0])}span[0]._i=i;span=$("<span />").addClass(css.vote).addClass(css.voteSelected).css(cssV).appendTo(sel);if(o.readOnly){span.addClass(css.voteReadOnlySelected)}if(cssi&&cssi[1]){span.addClass(cssi[1])}span[0]._i=i;if(me._swap&&!me._selSwap){me._selSwap=span}if(hov){span=$("<span />").addClass(css.vote).addClass(css.voteHover).css(cssV).appendTo(hov);if(cssi&&cssi[2]){span.addClass(cssi[2])}span[0]._i=i;if(me._swap&&!me._hovSwap){me._hovSwap=span}}}me._size=width;width*=count;elem.css({height:height+"px",width:width+"px"});try{height+=(i=Math.max(elem.outerHeight()-elem.innerHeight(),0))>10?2:i;width+=(i=Math.max(elem.outerWidth()-elem.innerWidth(),0))>10?2:i}catch(ex){}elem0.css({height:height+"px",width:width+"px"});div.css(sto);if(!val){me.value(me.value())}},_doEvt:function(evt,type){var val,me=this,o=this.options;if(o.readOnly||!me._sel){return}if(type===5){if(me._hov){me._hasHov=false;me._hov[0].style.visibility="hidden"}me._doVal(me._count(o),evt,false);return}if(type===4){if(me._hov){me._hasHov=false;me._hov[0].style.visibility="hidden"}me._doVal(0,evt,false);return}if(type===3){if(!me._hov){return}me._hasHov=false;me._hov[0].style.visibility="hidden";return}val=me._valFromEvt(evt);if(val<0){return}if(type===1){if(!me._sel){return}if(me._hov){val=me._lastHov||val}if(val){me._doVal(val,evt,false);if(me._hov){me._doVal(val,evt,1)}}}if(type===2){if(!me._hov){return}me._lastHov=val;if(!me._hasHov){me._hasHov=true}me._hov[0].style.visibility="visible";me._doVal(val,evt,1)}evt.preventDefault()},_setOption:function(key,val){var count,o=this.options;if(o[key]===val){return}count=this._count(o);o[key]=val;if(key.indexOf("vote")===0||key==="readOnly"){this._doVotes(o)}if(key.indexOf("value")>=0){this._doVal(val,key.length>6)}},_evtOffset:function(evt){var val,oEvt=evt.originalEvent||evt,offset="offsetX";if(_aNull(val=evt[offset])){if(_aNull(val=oEvt[offset])){if(_aNull(val=evt[offset="layerX"])){val=oEvt[offset]}}}return val},_valFromEvt:function(evt){var plus,fix,val,offset,i,targ=evt?evt.target:null,o=this.options,count=this._count(o);i=targ&&targ.nodeName==="SPAN"?targ._i:null;if(i===null||i===undefined){return-1}if(!targ.unselectable){targ.unselectable="on"}offset=this._evtOffset(evt);if(evt.originalEvent&&evt.originalEvent.type.indexOf("touch")!==-1||evt.type.indexOf("touch")!==-1){if(offset<=0){offset+=evt.clientX+.05}if(offset<=0||isNaN(offset)){if(isNaN(offset)){offset=evt.clientX-$(targ.parentNode).offset().left}else{offset=evt.clientX-this._size+.05}}if(this._swap){fix=parseInt(targ.parentNode.style.marginLeft.replace("px",""),10);if(isNaN(fix)){fix=0}offset-=fix}}else{plus=this._size;fix=-1*parseInt(targ.parentNode.style.marginLeft.replace("px",""),10);if(isNaN(fix)){fix=0}if(this._swap){offset+=fix}plus*=this._swap?count-i-1:i;fix=-targ.offsetLeft;if(this._swap){offset-=fix}if(this._swap&&$.browser.mozilla&&fix<plus){offset+=fix}if(plus>offset&&!this._swap){offset+=plus}offset+=.05}val=this._swap?Math.floor(offset/this._size):Math.ceil(offset/this._size);if(this._swap){val=count-val}return Math.max(Math.min(val,count),0)},_doVal:function(val,evt,hov){var count,o=this.options,style=hov?this._hov:this._sel,size=this._size,swap=this._swap?hov?this._hovSwap:this._selSwap:null,id,inp;if(style){style=style[0];if(style){style=style.style}}if(!style){return}count=this._count(o);if(isNaN(val)){val=-1}if(evt&&evt.type){if((hov?this._valH:this._val)===val||!this._trigger(hov?"hoverChange":"valueChange",evt,{value:val,oldValue:hov?this._valH:this._val})){return}}if(hov){this._valH=val}else{this._val=o.value=val;id=o.inputName;if(id){inp=$('input[name="'+id+'"]');if(inp.length===0){inp=$('<input type="hidden" name="'+id+'" />').appendTo(this.element.parent())}inp.val(val)}}if(swap){val=count-val}val=o.readOnly?Math.round(val*2)/2:Math.round(val);val=Math.floor(val*size+.3);val+="px";if(swap){swap=swap[0];if(swap){swap=swap.style}}if(swap){style.left=val;swap.marginLeft="-"+val}else{style.width=val}},value:function(val){if(typeof val!=="number"){return this._val}this._doVal(val,false);return this},destroy:function(){var old=this._old,e=this.element;if(!this._elem){return this}this._elem.remove();e[0].style.width=old.width;e[0].style.height=old.height;e[0].innerHTML=old.html;$.Widget.prototype.destroy.apply(this,arguments);this._elem=this._sel=this._selSwap=this._hov=this._hovSwap=this._evts=null;return this}});function _igmRatingAutoInit(e){var target=document;if(e){target=e.target}$.each($(target).find(":jqmData(role='igrating')"),function(key,value){var $rating=$(value),optObj={},args;args={element:$rating,options:optObj};$(document).trigger("_igRatingOptions",args);$rating.igRating(args.options)})}if($.ig&&$.ig.loader){$.ig.loader().preinit(_igmRatingAutoInit)}$(document).bind("pagecreate create",_igmRatingAutoInit);$.extend($.mobile.igRating,{version:"14.2.20142.1018"})})(jQuery);