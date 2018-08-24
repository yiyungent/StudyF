$(function () {
	// start set message
	if ($.cookie("inputMessage")) {
		$("#inputMessage").attr("placeholder", $.cookie("inputMessage"));
	} else {
		$.cookie("inputMessage", $("#inputMessage").attr("placeholder"), { expires: 21, path: '/' });
	}

	$("#inputMessage").change(function () {
		var msg = "";
		if ($.trim($(this).val()) != "") {
			msg = $.trim($(this).val());
			$(this).attr("placeholder", msg);
			$.cookie("inputMessage", msg, { expires: 21, path: '/' });
		}
	});
	// end set message

	// start send message
	$("#btnClock").click(function (event) {
		event.preventDefault();
		$.ajax({
			url: "/API/TimeMsg",
			type: "POST",
			data: { "inputMessage": $.cookie("inputMessage") },
			dataType: "json",
			success: function (data) {
				if (data.isSuccess) {
					$("#btnClock").addClass("disabled");
					$("#btnClock").prop("disabled", true);
				}
				loadRanking();
			}
		});
	});
	// end send message

	// start load ranking
	loadRanking();
	function loadRanking() {
		$("#ranking-list").html("");
		$.ajax({
			url: "/API/GetRankingList",
			type: "POST",
			data: {},
			dataType: "json",
			success: function (data) {
				if (data.isSuccess) {
					showRanking(data.dataList);
				}
			}
		});
	}

	function showRanking(dataList) {
		for (var i = 0; i < dataList.length; i++) {
			$("#ranking-list").append('<tr><td class="text-nowrap">' + (i + 1) + '</td><td class="text-nowrap">' + dataList[i].sendTime + '</td><td>' + dataList[i].sendMessage + '</td></tr>');
		}
	}
	// end load ranking
});