<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CrmMenuWebUserControl.ascx.cs" Inherits="VendasWeb.usercontrol.CrmMenuWebUserControl" %>


<div id="mainnav-menu-wrap">
						<div class="nano">
							<div class="nano-content">
								<ul id="mainnav-menu" class="list-group">
						
									<!--Category name-->
									<li class="list-header">Menu</li>
						
                                    <asp:Literal ID="MenuLiteral" runat="server"></asp:Literal>
                               
									<!--Menu list item-->
									<!--<li class="active-link">
										<a href="#">
											<i class="fa fa-list-alt fa-lg"></i>
											<span class="menu-title">
												<strong>Clientes</strong>
											</span>
										</a>
									</li>-->

									

								</ul>

							</div>
						</div>
					</div>
