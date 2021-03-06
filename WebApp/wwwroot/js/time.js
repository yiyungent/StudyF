﻿$(function () {
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

	// start send punchClock message
	$("#btnPunch").click(function (event) {
		event.preventDefault();
		$.ajax({
			url: "/API/PunchClock",
			type: "POST",
			data: { "inputMessage": $.cookie("inputMessage") },
			dataType: "json",
			success: function (data) {
				console.log(data);
				if (data.code > 0) {
					$("#btnPunch").text(data.msg);
					$("#btnPunch").addClass("disabled");
					$("#btnPunch").prop("disabled", true);
				} else {
					alert(data.msg);
				}
				loadRanking();
			}
		});
	});
	// end send punchClock message

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
				if (data.code > 0) {
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