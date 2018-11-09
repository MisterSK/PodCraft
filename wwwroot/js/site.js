// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const site_proto = location.protocol;
const site_host =  location.hostname;
const site_port =  location.port;
const site_uri = site_proto+'//'+site_host+(site_port ? ':'+location.port: '')+'/api/users';

let users = null;
function getUserCount(site_data) {
  const site_el = $("#site_counter");
  let firstname = "user";
  if (site_data) {
    if (site_data > 1) {
      firstname = "user(s)";
    }
    site_el.text(site_data + " " + firstname);
  } else {
    site_el.text("No " + firstname);
  }
}

$(document).ready(function() {
  getData();
});

function getData() {
  $.ajax({
    type: "GET",
    url: site_uri,
    cache: false,
    success: function(site_data) {
      const search_tBody = $("#users");

      $(search_tBody).empty();

      getUserCount(site_data.length);

      $.each(site_data, function(key, user) {
        const site_tr = $("<tr></tr>")
          .append($("<td></td>").text(user.firstName))
          .append($("<td></td>").text(user.lastName))
          .append($("<td></td>").text(user.emailAddress))
          .append($("<td></td>").text(user.dateOfBirth))
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

        site_tr.appendTo(search_tBody);
      });

      users = site_data;
    }
  });
}

function addUser() {
    const user = {
    firstName: $("#add-firstname").val(),
    lastName: $("#add-lasttname").val(),
    emailAddress: $("#add-emailaddress").val(),
    dateOfBirth: $("#add-dateofbirth").val()
    };

    $.ajax({
    type: "POST",
    accepts: "application/json",
    url: site_uri,
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
    url: site_uri + "/" + id,
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
        $("#edit-firstname").val(user.firstName);
        $("#edit-lastname").val(user.lastName);
        $("#edit-emailaddress").val(user.emailAddress);  
        $("#edit-dateofbirth").val(user.dateOfBirth);
    }
        });
        $("#spoiler").css({ display: "block" });
    }

    $(".my-form").on("submit", function() {
        const user = {
        id: $("#edit-id").val(),
        firstName: $("#edit-firstname").val(),
        lastName: $("#edit-lastname").val(),
        emailAddress: $("#edit-emailaddress").val(),
        dateOfBirth: $("#edit-dateofbirth").val(),
    };

    $.ajax({
        url: site_uri + "/" + $("#edit-id").val(),
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