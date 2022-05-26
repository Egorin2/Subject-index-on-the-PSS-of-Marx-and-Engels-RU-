const express = require('express');
const path = require('path');
const fs = require('fs/promises');
const jsdom = require('jsdom');
const {JSDOM} = jsdom;

const app = express();

let dom = new JSDOM("<!DOCTYPE html><head></head><body><p>Hello world</p></body>");
let document = dom.window.document;
function takeScript(){
let scrip = document.createElement("script");
scrip.innerHTML = 'function takeClick (elem){let content = elem.parentElement.children[1];elem.onclick = function() {if (content.style.display ===""){content.style.display ="block";}if (content.style.display ==="block"){content.style.display = "none";  } else {    content.style.display = "block";  }  }};function stepElem(str){    let arr = document.getElementsByClassName(str);    for (let i =0; i<arr.length;i++){ takeClick(arr[i]);    }  }  stepElem("object-name");  stepElem("rubric-name");  stepElem("categori-name");'
document.body.append(scrip);
}
let go = false;
let pathToFile = path.join(__dirname, "word");
async function blow(){
fs.readdir(pathToFile).then(
  async value =>{
    let arr = [];
    for (let i = 0; i<value.length;i++){
      let folder = path.join(pathToFile, value[i]);
      console.log("Дошёл");
      let a = await fs.readdir(folder).then(
        ww =>{
          let array = [];
          ww.forEach(foo =>{
          let filename = path.join(folder, foo);
          array.push(filename);
          })
          return array;})
      a.forEach(element => {
        arr.push(element);
      });
      }
    console.log("Перешёл. Таков порядок");
    console.log("прошлись по папкам");
    return arr;
  }
).then(async value =>{
  console.log(value.length);
  console.log("Закончили обработку");
  console.log("Должен вылететь последним");
  let index = 0;
  for (let i =0; i<value.length;i++){
    await fs.readFile(value[i],"utf-8").then(
      openfile =>{
        let arr = openfile.split("\r\n");
        let div = new DivElement(arr);
        document.body.append(div.dom);
      });
      index++;
      if (go){
        console.log("ОШИБКА")
      }
      console.log("Обработали "+i+" из "+value.length);
  } 
  return index; 
}).then(value =>{
  go = true;
  console.log(value);
  console.log("Край");
  takeScript();
  fs.writeFile(path.join(__dirname, "index.html"), dom.serialize()).then(file =>{
    console.log("Записали");
  })
})
}
blow();


class DivElement{
  document = new JSDOM().window.document;
  lastRubric;
  isRubric;
  lastCategory;
  content;
  contentDiv;
  constructor(arr){
    this.div = document.createElement("div");
    let name = document.createElement("h2");
    name.innerHTML = arr[0];
    name.classList.add('object-name');
    name.classList.add('text-content');    
    this.div.append(name);
    let content = document.createElement('div');
    content.classList.add("content");
    content.style.display = "none";
    this.content = content;
    this.div.append(content);
    for (let i=1; i<arr.length;i++){
      if(arr[i].includes("[Rubric]")){
        this.isRubric = true;
        let div = document.createElement('div');
        div.classList.add("rubric");
        let span = document.createElement("h3");
        span.innerHTML = arr[i].replace("[Rubric] ","");
        span.classList.add('rubric-name');
        span.classList.add('text-content');
        div.append(span);
        let content = document.createElement("div");
        content.classList.add("content");
        div.append(content);
        this.lastRubric = content;
        this.content.append(div);
        continue;
      }
      if (arr[i].includes("[Categori]")){
        if (arr[i]==="[Categori] "){
          this.lastCategory = this.content;
          continue;
        }
        let div = document.createElement("div");
        div.classList.add("categori");
        let span = document.createElement("h4");
        span.innerHTML = arr[i].replace("[Categori] ", "");
        span.classList.add('categori-name');
        span.classList.add('text-content');
        div.append(span);
        let content = document.createElement("div");
        content.classList.add("content");
        content.style.display = "none"; 
        div.append(content);
        this.lastCategory = content;
        if(this.isRubric){
          this.lastRubric.append(div);
          continue;
        }
        this.content.append(div);
        continue;
      }
      if(arr[i].includes("[PU]")){
        this.lastCategory.append(DivElement.PU(arr[i]));
        continue;
      }
      if (arr[i].includes("[Link]")){
        if(arr[i] === "[Link] "){
          continue;
        }
        if (arr[i] === "[Link]"){
          continue;
        }
        let mass = DivElement.getLink(arr[i]);
        mass.forEach(elem =>{
          if (elem ===" "){
            return;
          }
          if (elem === null){
            return;
          }
          let tom;
          try {
          tom = DivElement.reTom(elem);
           }
          catch (e){
            console.log("Значит так. Бля");
            console.log("Загоняли вот это бля");
            console.log(mass);
            console.log("Вот тут бля");
            console.log(arr[0]);
          }
          try{
          tom = DivElement.Link(tom);}
          catch (e){
            console.log("Значит так. Бля");
            console.log("Загоняли вот это бля");
            console.log(tom);
            console.log("Вот тут бля");
            console.log(arr[0]);            
          }
          this.lastCategory.append(tom);
        })
        continue;
      }
    }
  }
  get dom() {
    return this.div;
  }
  static getLink(str){
    let string = str.replace("[Link] ", "");
    string = string.trim();
    if (string[string.length-1]===";"){
    string = string.substring(0, string.length-1);
    }
    let arr = string.split(";");
    return arr;
  }
  static reTom(str){
    let arr = str.split(":");
  let obj = {
    tom: arr[0].trim()
  }
  arr = arr[1].split(',');
  arr.forEach(element => {
    element = element.trim();
  });
  obj.page = arr;
  return obj;
  }
  static Link(obj){
    let str = document.createElement("p");
    str.classList.add("number-content");
    str.classList.add("text-content");
    let span = document.createElement("span");
    span.classList.add("tom-number");
    span.innerHTML = obj.tom;
    str.append(span);
    str.innerHTML = str.innerHTML + ": ";
    obj.page.forEach(elem =>{
      let page = document.createElement("span");
      page.classList.add("page-number");
      page.innerHTML = elem;
      str.append(page);
      str.innerHTML = str.innerHTML + ", ";
    })
    str.innerHTML = str.innerHTML.substring(0,str.innerHTML.length-2);
    return str;
  }
  static PU(str){
    let pu = document.createElement("div");
    pu.classList.add("pu-content");
    if (str.includes("[Link]")){
      str = str.substring(0,str.indexOf("[Link]"));
    }
    let p = document.createElement("p");
    p.classList.add("pu-text");
    p.innerHTML = str.replace("[PU]","");
    pu.append(p);
    return pu;
  }

}


app.get('/', async(req, res) => {
  document.body.append(scrip);
  res.send(dom.serialize());
  })

app.listen(3000, () => {
    console.log('Запуск сервера успешен. Порт ' + 3000);
   });