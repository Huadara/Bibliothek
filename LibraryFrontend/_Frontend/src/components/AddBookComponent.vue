<template>
  <div>
    <h1>BOOK</h1>
    <b-container>
      <b-row>
        <b-col>
          <h4>Title:</h4>
        </b-col>
        <b-col>
          <input v-model="title">
        </b-col>
      </b-row>
      <b-row>
        <b-col>
          <h4>Author:</h4>
        </b-col>
        <b-col>
          <input v-model="author">
        </b-col>
      </b-row>
      <b-row>
        <b-col>
          <h4>Publisher:</h4>
        </b-col>
        <b-col>
          <input v-model="publisher">
        </b-col>
      </b-row>
      <b-row>
        <b-col>
          <h4>ISBN:</h4>
        </b-col>
        <b-col>
          <input v-model="isbn">
        </b-col>
      </b-row>
      <b-row>
        <b-col>
          <h4>Price:</h4>
        </b-col>
        <b-col>
          <input v-model="price">
        </b-col>
      </b-row>
      <b-row>
        <b-col>
          <b-button :variant="primary" @click="testget">Add Book</b-button>
        </b-col>
      </b-row>
    </b-container>
    <h1>{{posts}}</h1>
  </div>
</template>

<script>
  import axios from 'axios';

  export default {
    name: "AddBookComponent",
    data() {
      return {
        title: "",
        author: "",
        publisher: "",
        isbn: "",
        price: "",
        response: "",
        error: "",
        posts: []
      }
    },
    methods: {
      addBook(title, author, publisher, isbn, price) {
        const json = `{ "title": "${title}", "author": "${author}", "publisher": "${publisher}", "isbn": "${isbn}", "price": ${price}}`
        console.log(Console);

        axios.post('http://localhost:5000/library/books', json,
          {

            headers: {
              'Content-type': 'application/json'

            }
          });
      }
      ,
      testget() {
        return axios.get('http://localhost:8080/library/books/1',
          {
            headers:{
              'Access-Control-Allow-Origin': 'http://localhost:8080/'
            }
          })
          .then(function (response) {
            this.setState({posts: response.data});
            console.log(this.posts);
          })
          .catch(function (error) {

          });
      }
    },
  }
</script>

<style scoped>

</style>

