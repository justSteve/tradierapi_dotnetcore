# tradierapi_dotnetcore

Retail brokers' trading apps are generalized to handle trading of any instrument from Stocks, Futures, and options. But they have not created
interfaces that are adaquate for trading 0DTE options given the speed required to make adjustments and avoid losses.

This project attempts to create an app that is solely focused on needs of the 0DTE trader by translating a generic Object Model 
(holding the 0DTE-specific info) into a model recognized by the broker's API. The result will be a UI controlled at the CLI and/or 
naturual language level.
