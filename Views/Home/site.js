
const uri = "api/users";
let users = null;
function getCount(data) {
  const el = $("#counter");
  let firstname = "users";
  if (data) {
    if (data > 1) {
      firstname = "users";
    }
    el.text(data + " " + firstname);
  } else {
    el.text("No " + firstname);
  }
}

$(document).ready(function() {
  getData();
});

function getData() {
  $.ajax({
    type: "GET",
    url: uri,
    cache: false,
    success: function(data) {
      const tBody = $("#users");

      $(tBody).empty();

      getCount(data.length);

      $.each(data, function(key, user) {
        const tr = $("<tr></tr>")
          .append($("<td></td>").text(user.firstname))
          .append($("<td></td>").text(user.lastname))
          .append($("<td></td>").text(user.emailaddress))
          .append($("<td></td>").text(user.dateofbirth))
          .append(
            $("<td></td>").append(
              $("<button>Edit</button>").on("click", function() {
                editUser(user.id);
              })
            )
          )
          .append(
            $("<td></td>").append(
              $("<button>Delete</button>").on("click", function() {
                deleteUser(user.id);
              })
            )
          );

        tr.appendTo(tBody);
      });

      users = data;
    }
  });
}

function addUser() {
  const user = {
    name: $("#add-firstname").val(),
    name: $("#add-lasttname").val(),
    name: $("#add-emailaddress").val(),
    name: $("#add-dateofbirth").val()
  };

  $.ajax({
    type: "POST",
    accepts: "application/json",
    url: uri,
    contentType: "application/json",
    data: JSON.stringify(user),
    error: function(jqXHR, textStatus, errorThrown) {
      alert("Opps... Something went wrong!");
    },
    success: function(result) {
      getData();
      $("#add-user").val("");
    }
  });
}

function deleteUser(id) {
  $.ajax({
    url: uri + "/" + id,
    type: "DELETE",
    success: function(result) {
      getData();
    }
  });
}

function editUser(id) {
  $.each(users, function(key, user) {
    if (user.id === id) {
        $("#edit-id").val(user.id);
        $("#edit-firstname").val(user.name);
        $("#edit-lastname").val(user.name);
        $("#edit-emailaddress").val(user.name);  
        $("#edit-dateofbirth").val(user.name);
    }
  });
  $("#spoiler").css({ display: "block" });
}

$(".my-form").on("submit", function() {
  const user = {
    id: $("#edit-id").val(),
    firstname: $("#edit-firstname").val(),
    lastname: $("#edit-lastname").val(),
    emailaddress: $("#edit-emailaddress").val(),
    dateofbirth: $("#edit-dateofbirth").val(),
  };

  $.ajax({
    url: uri + "/" + $("#edit-id").val(),
    type: "PUT",
    accepts: "application/json",
    contentType: "application/json",
    data: JSON.stringify(user),
    success: function(result) {
      getData();
    }
  });

  closeInput();
  return false;
});

function closeInput() {
  $("#spoiler").css({ display: "none" });
}