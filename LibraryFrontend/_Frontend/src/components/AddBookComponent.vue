<template>
  <div>
    <h1>Add Book</h1>
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
          <h4>ISBN:</h4>
        </b-col>
        <b-col>
          <input v-model="isbn">
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
          <h4>Price:</h4>
        </b-col>
        <b-col>
          <input v-model="price">
        </b-col>
      </b-row>
      <b-row>
        <b-col>
          <b-button :variant="primary" @click="addBook">Add Book</b-button>
        </b-col>
      </b-row>
    </b-container>
    <span>{{msg}}</span>
  </div>
</template>

<script>
import axios from 'axios'

export default {
  name: 'AddBookComponent',
  data () {
    return {
      title: '',
      author: '',
      isbn: '',
      publisher: '',
      price: '',
      msg: ''
    }
  },
  methods: {
    addBook () {
      const self = this
      axios.post('http://localhost:5000/library/books', {
        title: this.title,
        author: this.author,
        publisher: this.publisher,
        isbn: this.isbn,
        price: this.price
      }).then(function (response) {
        if (response != null) console.log('response')
        self.msg = 'Add successful'
      }).catch(function (error) {
        if (error != null) console.log('error')
        self.msg = 'Please check your entries'
      })
    }
  }
}
</script>

<style scoped>

</style>
