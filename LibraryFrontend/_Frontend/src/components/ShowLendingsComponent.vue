<template>
  <div v-bind:style="{ color: 'white' }">
    <h1>
      Current Lendings
      <b-button :variant="primary" @click="fetchLendings">Fetch Lendings</b-button>
    </h1>
    <table v-bind:style="tableStyle">
      <thead>
      <tr>
        <th v-bind:style="tableHeader">Customer</th>
        <th v-bind:style="tableHeader">Book</th>
        <th v-bind:style="tableHeader">Lending Date</th>
      </tr>
      </thead>
      <tbody>
      <!-- eslint-disable-next-line -->
      <tr v-for="lending in lendings">
        <td v-bind:style="tableCell">{{lending.customer_name}}</td>
        <td v-bind:style="tableCell">{{lending.book_title}}</td>
        <td v-bind:style="tableCell">{{lending.start_date}}</td>
      </tr>
      </tbody>
    </table>
    <span>{{msg}}</span>
  </div>
</template>

<script>
export default {
  name: 'ShowLendingsComponent',
  data () {
    return {
      msg: '',
      lendings: '',
      tableStyle: {
        'width': '100%',
        'border': '3px solid'
      },
      tableHeader: {
        'text-transform': 'uppercase',
        'text-align': 'left',
        'border-bottom': '3px solid'
      },
      tableCell: {
        'text-align': 'left',
        'padding': '8px',
        'border-right': '2px solid'
      }
    }
  },
  methods: {
    fetchLendings () {
      const self = this
      self.$axios
        .get('http://localhost:5000/library/books/GetRelevantLendings')
        .then(function (lendingResponse) {
          self.msg = 'Fetched current Lendings'
          self.lendings = lendingResponse.data
        })
    }
  }
}
</script>

<style scoped>

</style>
