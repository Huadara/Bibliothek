<template>
  <div v-bind:style="{ color: 'white' }">
    <h1>Add Supplier</h1>
    <b-container>
      <b-row>
        <b-col>
          <h4>Company Name:</h4>
        </b-col>
        <b-col>
          <input v-model="company_name">
        </b-col>
      </b-row>
      <b-row>
        <b-col>
          <h4>Address:</h4>
        </b-col>
        <b-col>
          <input v-model="address">
        </b-col>
      </b-row>
      <b-row>
        <b-col>
          <b-button :variant="primary" @click="addSupplier">Submit</b-button>
        </b-col>
      </b-row>
    </b-container>
    <span>{{msg}}</span>
  </div>
</template>

<script>
import axios from 'axios'

export default {
  name: 'AddSupplierComponent',
  data () {
    return {
      company_name: '',
      address: '',
      msg: ''
    }
  },
  methods: {
    addSupplier () {
      const self = this
      axios.post('http://localhost:5000/library/suppliers', {
        'company_name': this.company_name,
        'address': this.address
      }).then(function (response) {
        if (response != null) console.log('response')
        self.msg = response.data.message
      }).catch(function (error) {
        if (error != null) console.log('error')
        self.msg = 'Please check your entries'
      })
      self.company_name = ''
      self.address = ''
    }
  }
}
</script>

<style scoped>

</style>
